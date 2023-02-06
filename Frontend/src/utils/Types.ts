export type Company = {
	id: string;
	name: string;
	street: string;
	phoneNumber: string;
	city: string;
	country: string;
	zip: string;
	tin: string;
	vatin: string;
	hasApprovedRegistration: boolean;
	priceCategoryName?: string;
};

export const OrderStates = [
	"OPEN",
	"IN PROCESS",
	"STORNO",
	"READY",
	"CLOSED",
] as const;
export type OrderState = (typeof OrderStates)[number];

export type Order = {
	id: string;
	variableNumber: string;
	dateCreated: Date;
	totalPrice: number;
	status: OrderState;
};

export type PriceCategory = {
	id: string;
	name: string;
};

export type CartItem = {
	id: string;
	productName: string;
	imagePath: string;
	count: number;
	unitPrice: number;
	vat: number;
	totalPrice: number;
};

export type Cart = {
	id: string;
	totalPrice: number;
	cartItems: CartItem[];
};

export type ProductPrice = {
	productPriceId: string;
	priceCategoryId: string;
	price: number;
	priceCategoryName: string;
};

export type Product = {
	productPrices?: ProductPrice[];
	id?: string;
	name?: string;
	weight?: number;
	vat?: number;
	description?: string;
	imageURI?: string;
};
