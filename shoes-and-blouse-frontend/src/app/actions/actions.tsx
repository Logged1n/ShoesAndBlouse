"use server"

import axios from "axios";
import {Category, Product} from "@/app/_types/api_interfaces";

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
