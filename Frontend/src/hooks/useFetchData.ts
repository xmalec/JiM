import { useState, useCallback, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import {
	InternalError,
	UnauthorizedError,
	GetRequest,
	AnyRequest,
} from "../utils/ApiConnector";
import { Cart, Company, Order, PriceCategory, Product } from "../utils/Types";

import { User } from "./useLoggedInUser";

type RequestResult<T> = {
	unauthorized: boolean;
	loading: boolean;
	ok: boolean;
	data?: T;
	refresh: () => void;
};

export const postData = async (endpoint: string, bodyData?: object) =>
	await AnyRequest(endpoint, bodyData ?? {}, "POST");

export const putData = async (endpoint: string, bodyData?: object) =>
	await AnyRequest(endpoint, bodyData ?? {}, "PUT");

export const deleteData = async (endpoint: string, bodyData?: object) =>
	await AnyRequest(endpoint, bodyData ?? {}, "DELETE");

export const useFetchData = <T>(endpoint: string): RequestResult<T> => {
	const [data, setData] = useState<T>();
	const [loading, setLoading] = useState(true);
	const [ok, setOk] = useState(false);
	const navigate = useNavigate();
	const refresh = useCallback(async () => {
		try {
			setData(await GetRequest<T>(endpoint));
			setOk(true);
		} catch (err) {
			if (err instanceof InternalError) {
				console.error(err.message);
			} else if (err instanceof UnauthorizedError) {
				navigate("/");
				return;
			}
		} finally {
			setLoading(false);
		}
	}, []);
	useEffect(() => {
		refresh();
	}, []);
	return {
		data,
		loading,
		unauthorized: false,
		ok,
		refresh,
	};
};

export const useCompany = (id: string) =>
	useFetchData<Company>(`/api/companies/${id}`);

export const useOrder = (id: string) =>
	useFetchData<Order>(`/api/orders/${id}`);

export const useProduct = (id: string | undefined) => {
	if (id === undefined) {
		return useFetchData<Product>(`/api/product/create`);
	} else {
		return useFetchData<Product>(`/api/products/${id ?? "0"}`);
	}
};

export const useCart = () => useFetchData<Cart>(`/api/cart/detail`);

export const usePriceCategoryOptions = () =>
	useFetchData<PriceCategory[]>("/api/priceCategory/all") ?? [];

export const login = (email: string, password: string) =>
	postData("/api/auth/login", { email, password }).then(
		(response) => response.json() as Promise<User>
	);

export const register = (values: object) =>
	postData("/api/company/register", values);

export const deleteProduct = (id: string) => deleteData(`/api/product/${id}`);

export const rejectCompany = (id: string) =>
	postData(`/api/company/reject/${id}`);

export const approveCompany = (id: string, priceCategoryId: string) =>
	postData("/api/company/approve", { id, priceCategoryId });

export const addUser = (values: object, companyId: string) =>
	postData(`/api/company/${companyId}/add-user`, values);

export const saveProduct = (values: FormData, id?: string) =>
	id
		? putData(`/api/products/${id}`, values)
		: postData("/api/product/create", values);

export const updateCart = (productId: string, count: number) =>
	postData("/api/cart/update", { productId, count });

export const changeState = (orderState: number, orderId: string) =>
	putData("/api/orders/order-state", { orderId, orderState });

export const confirmOrder = () => postData("/api/cart/confirm");
