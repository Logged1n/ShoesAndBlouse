export interface Price {
    currency: string;
    amount: number;
}

export interface Product {
    id: number;
    name: string;
    description: string;
    price: Price;
    categories: Record<number, string>;
    photoUrl: File;
}

export interface Category {
    id: number;
    name: string;
    description: string;
    products: Record<number, string>;
}

export interface AddCategoryFormProps {
    products: Product[];
}