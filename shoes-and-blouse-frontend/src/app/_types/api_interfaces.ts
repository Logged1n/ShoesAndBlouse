export interface Price {
    currency: string;
    amount: number;
}
export interface OrderForm{
    id: number;
    createdAt: string;
    modifiedAt: string;
    status: number;
    shippingAddressId: number;
    billingAddressId: number;
    orderDetails: number;
    total: Price;
}
export interface cartItem{
    productId: number;
    qty: number;
}
export interface Cart{
    total: Price;
    cartItems: cartItem[];
}
export interface ProductForm {
    id: number;
    name: string;
    description: string;
    price: Price;
    categories: Record<number, string>;
    photoUrl: File;
}

export interface Product {
    id: number;
    name: string;
    description: string;
    price: Price;
    categories: Record<number, string>;
    photoUrl: string;
}

export interface Category {
    id: number;
    name: string;
    products: Record<number, string>;
    productIds: string[];
}

export interface LoginDetails {
    email: string;
    password: string;
}

export interface User {
    id: number;
    name: string;
    email: string;
    surname: string;
}

export interface Review {
    id: string;
    score: number;
    productId: string;
    userId: string;
    title: string;
    description: string;
}
