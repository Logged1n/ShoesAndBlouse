"use client"

import axios from "axios";
import {useEffect, useState} from "react";
import ToolBar from "@/components/ToolBar";
interface Price {
    currency: string;
    amount: number;
}

interface Product {
    id: number;
    name: string;
    description: string;
    price: Price;
    categories: Record<string, string>;
    photoUrl: string;
}

export default function ProductsPanel() {
    const [products, setProducts] = useState<Product[]>([]);
    const makeApiCall = async (): Promise<void> => {
        const data = await axios.get<Product[]>(`backendAPI/v1/Product/GetALL`);
        setProducts(data.data)
    }

    useEffect(() => {
        makeApiCall();
    }, []);

    return (
        <div>
            <ToolBar />
        </div>
    );
}