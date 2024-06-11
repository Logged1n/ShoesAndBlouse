"use server"

import axios from "axios";
import { Category, Product, User } from "../_types/api_interfaces"; // Ścieżka do api_interfaces

// Pobieranie wszystkich kategorii
export async function GetCategories(): Promise<Category[]> {
    try {
        const { data } = await axios.get<Category[]>(`${process.env.API_URL}/api/v1/Category/GetAll`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return [];
    }
}

// Pobieranie wszystkich produktów
export async function GetAllProducts(): Promise<Product[]> {
    try {
        const { data } = await axios.get<Product[]>(`${process.env.API_URL}/api/v1/Product/GetAll`);
        console.log('Produkty z API:', data);  // Dodane logowanie
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu produktów: ', error);
        return [];
    }
}

// Pobieranie kategorii po ID
export async function GetCategoryById(categoryId: string): Promise<Category | null> {
    try {
        const { data } = await axios.get<Category>(`${process.env.API_URL}/api/v1/Category/GetById/${categoryId}`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return null;
    }
}

// Pobieranie produktu po ID
export async function GetProductById(productId: string): Promise<Product | null> {
    try {
        const { data } = await axios.get<Product>(`${process.env.API_URL}/api/v1/Product/GetById/${productId}`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu produktu: ', error);
        return null;
    }
}

// Pobieranie użytkownika po ID
export async function GetUserById(userId: string): Promise<User | null> {
    try {
        const { data } = await axios.get<User>(`${process.env.API_URL}/api/v1/User/GetUserById/${userId}`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu użytkownika: ', error);
        return null;
    }
}
