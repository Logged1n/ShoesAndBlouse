export interface Price {
    currency: string;
    amount: number;
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