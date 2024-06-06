export interface Price {
    currency: string;
    amount: number;
}

export interface Product {
    id: string;
    name: string;
    description: string;
    price: Price;
    categories: Record<string, string>;
    photoUrl: string;
}

export interface Category {
    id: string;
    name: string;
    products: Record<string, string>;
}

export interface AddCategoryFormProps {
    products: Product[];
}
