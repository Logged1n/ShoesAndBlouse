"use client"

import axios from "axios";
import {useState} from "react";
import {Product} from "@/app/_types/api_interfaces";

export default function Dummy() {
    const [products, setProducts] = useState<Product[]>([]);
    const makeApiCall = async (): Promise<string> => {
         const data = await axios.get<Product[]>(`/backendAPI/api/v1/Product/GetAll`);
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
                            <tr key={product.id}>
                                <td key={product.id}>
                                    {product.id}
                                </td>
                                <td key={product.name}>
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