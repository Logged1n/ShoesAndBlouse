"use client"

import axios from "axios";
import {useState} from "react";

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

export default function Dummy() {
    const [products, setProducts] = useState<Product[]>([]);
    const makeApiCall = async (): Promise<string> => {
         const data = await axios.get<Product[]>(`backendAPI/v1/Product/GetALL`);
         console.log(data);
         setProducts(data.data);
        return JSON.stringify(data);
    }

    return (
        <div>
            <button onClick={makeApiCall}>Call</button>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>name</th>
                    </tr>
                </thead>
                <tbody>
                    
                    { products.length > 0 ?
                        products.map((product) => (
                            <tr>
                                <td key={product.id}>
                                    {product.id}
                                </td>
                                <td key={product.id}>
                                    {product.name}
                                </td>
                            </tr>
                                ))
                                :"Fetching products..."}
                           
                        </tbody>
                
            </table>
        </div>
    );
}