"use client"
import {GetProductById} from "@/app/actions/actions";
import {useEffect, useState} from "react";
import {Product} from "@/app/_types/api_interfaces";

export default function EditProduct ({ params }: { params: { id: string } }) {

    const [product, setProduct] = useState<Product>();
    //use
    useEffect(() => {
            async function fetchProduct(){
                const fetchedProduct = await GetProductById(params.id);
                setProduct(fetchedProduct);
            }
            fetchProduct();
        },
        [params.id]);

return (
    <div>
        {product != undefined ?
            <>
                wczytany produkcik do edycji jest super
            </>
            : "czekamy na wczytanie (moze byc ze nigdy sie nie wczyta, bo go nie ma)"}
    </div>
);

}