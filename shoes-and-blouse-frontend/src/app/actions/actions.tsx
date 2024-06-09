"use server"

import axios from "axios";
import {Category, Product, ProductForm} from "@/app/_types/api_interfaces";

export async function GetCategories(): Promise<Category[]> {
    try {
        const { data } = await axios.get<Category[]>(`${process.env.API_URL}/api/v1/Category/GetAll`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return [];
    }
}
export async function GetProducts(): Promise<ProductForm[]> {
    try{
        const {data } = await axios.get<ProductForm[]>(`${process.env.API_URL}/api/v1/Product/GetAll`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu produktów: ', error);
        return [];
    }
}
export async function GetCategoryById(categoryId: string): Promise<Category | undefined> {
    try {
        const { data } = await axios.get<Category>(`${process.env.API_URL}/api/v1/Category/GetCategoryById/${categoryId}`);
        return data;
    } catch (error) {
        console.error('Błąd przy pobieraniu kategorii: ', error);
        return undefined;
    }
}
export async function GetProductById(productId: string): Promise<Product | undefined> {
    try{
        const {data} = await axios.get<Product>(`${process.env.API_URL}/api/v1/Product/GetProductById/${productId}`);
        return data;
    }
    catch (error) {
        console.error('Błąd przy pobieraniu produktu: ', error);
        return undefined;
    }
}