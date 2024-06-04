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
    photoUrl: string;
}

export interface Category {
    id: number;
    name: string;
    products: Record<number, string>;
}

export interface AddCategoryFormProps {
    products: Product[];
}
export interface AddProductFormProps {
    name: string;
    description: string;
    currency: string;
    amount: number;
    categories: number[]; // Dodajemy kategorie jako tablicę numerów
}