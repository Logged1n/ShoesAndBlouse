"use client"

import axios from "axios";
import {useEffect, useState} from "react";

import {Product} from "@/app/_types/api_interfaces";

export default function ProductsPanel() {
    const [products, setProducts] = useState<Product[]>([]);
    const makeApiCall = async (): Promise<void> => {
        const data = await axios.get<Product[]>(`/backendAPI/v1/Product/GetAll`);
        setProducts(data.data)
    }

    useEffect(() => {
        makeApiCall();
    }, []);

    return (
        <div>
            
        </div>
    );
}