"use server"

import axios from "axios";
import {Category, Product, User} from "@/app/_types/api_interfaces";

export async function GetCategories(): Promise<Category[]> {
    try {
        const { data } = await axios.get<Category[]>(`${process.env.API_URL}/api/v1/Category/GetAll`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return [];
    }
}
export async function GetProducts(): Promise<Product[]> {
    try{
        const {data } = await axios.get<Product[]>(`${process.env.API_URL}/api/v1/Product/GetAll`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu produktów: ', error);
        return [];
    }
}
export async function GetCategoryById(categoryId: string): Promise<Category | null> {
    try {
        const { data } = await axios.get<Category>(`${process.env.API_URL}/api/v1/Category/GetCategoryById/${categoryId}`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return null;
    }
}
export async function GetProductById(productId: string): Promise<Product | null> {
    try{
        const {data} = await axios.get<Product>(`${process.env.API_URL}/api/v1/Product/GetProductById/${productId}`);
        return data;
    }
    catch (error) {
        console.error('Błąd przy pobieraniu produktu: ', error);
        return null;
    }
}
export async function GetUserById(userId: string): Promise<User | null> {
    try{
        const {data} = await axios.get<User>(`${process.env.API_URL}/api/v1/User/GetUserById/${userId}`);
        return data;
    }
    catch (error) {
        console.error('Błąd przy pobieraniu uzytkownika: ', error);
        return null;
    }
}