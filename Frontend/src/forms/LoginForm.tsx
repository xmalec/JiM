import { useState, FormEvent } from "react";
import { useNavigate } from "react-router-dom";

import { login } from "../hooks/useFetchData";
import useField from "../hooks/useField";
import useLoggedInUser from "../hooks/useLoggedInUser";
import { BadRequestError } from "../utils/ApiConnector";

const LoginForm = () => {
	const navigate = useNavigate();
	const [email] = useField("email", true);
	const [password] = useField("password", true);
	const [submitError, setSubmitError] = useState<string>();
	const [, setAuth] = useLoggedInUser();
	return (
		<form
			onSubmit={async (e: FormEvent) => {
				e.preventDefault();
				try {
					setAuth(await login(email, password));
					navigate("/");
					//setMessage("Successfully logged in");
				} catch (err) {
					if (err instanceof BadRequestError) {
						//etMessage("Login failed", "error");
					} else {
						setSubmitError(
							(err as { message?: string })?.message ??
								"Unknown error occurred"
						);
					}
				}
			}}
		>
			<input name="Email" type="email" />
			<input name="Password" type="password" />

			{submitError && <span>{submitError}</span>}
			<button type="submit">Sign In</button>
		</form>
	);
};

export default LoginForm;
