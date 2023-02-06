import SessionManager from "./SessionManager";

const handleErrors = (response: Response) => {
	if (!response.ok) {
		if (response.status === 500)
			throw new InternalError(`Internal error (${response.status})`);
		if (response.status === 401)
			throw new UnauthorizedError(`Internal error (${response.status})`);
		if (response.status === 400)
			throw new BadRequestError(
				`Bad request error error (${response.status})`
			);
		//throw Error('Unknown error occured');
	}
	return response;
};

const headers = (exludeCT: boolean) => {
	const headers = new Headers({
		Authorization: `Bearer ${SessionManager.getToken()}`,
	});
	if (!exludeCT) {
		headers.append("Content-Type", "application/json");
	} else {
		//headers.append('Content-Type', 'application/x-www-form-urlencoded');
	}
	return headers;
};

export const GetRequest = async <T>(endpoint: string) =>
	await Request(endpoint, "GET").then(
		(response) => response.json() as Promise<T>
	);

export const AnyRequest = async (
	endpoint: string,
	data: object,
	method: string
) => await Request(endpoint, method, data);

export const Request = async (
	endpoint: string,
	method: string,
	data?: object
) =>
	await fetch(`${process.env.REACT_APP_API_URL}/${trimEndpoint(endpoint)}`, {
		method,
		headers: headers(data instanceof FormData),
		body: data
			? data instanceof FormData
				? data
				: JSON.stringify(data)
			: undefined,
	})
		.then(handleErrors)
		.catch((error) => {
			console.error("Error:", error);
			throw error;
		});

const trimEndpoint = (endpoint: string) => {
	while (endpoint.charAt(0) === "/") {
		endpoint = endpoint.substring(1, endpoint.length);
	}
	return endpoint;
};

export class UnauthorizedError extends Error {
	constructor(msg: string) {
		super(msg);
	}
}

export class InternalError extends Error {
	constructor(msg: string) {
		super(msg);
	}
}

export class BadRequestError extends Error {
	constructor(msg: string) {
		super(msg);
	}
}
