"use client"
import axios from "axios";
import {Product, Review} from "@/app/_types/api_interfaces";
import {useEffect, useState} from "react";
import Link from "next/link";

export default function ProductPage({ params }: { params: { id: string } }) {

    const [productData, setProductData] = useState<Product>();
    const [reviewsData, setReviewsData] = useState<Review[]>([]);

    useEffect(() => {
        //calls the .NET api for a specific product data
        async function GetProduct(id: string) {
            try {
                const { data } = await axios.get<Product>(`/backendAPI/api/v1/Product/GetById/${id}`);
                console.log(data);
                setProductData(data);
            }
            catch(error)
            {
                console.error(error);
            }
        }

        async function GetProductReviews(id: string) {
            try {
                const data = await axios.get<Review[]>(`/backendAPI/api/v1/Review/GetAllProduct/${id}`);
                console.log(data);
                setReviewsData(data.data);
            }
            catch(error) {
                console.error(error);
            }
        }
        GetProduct(params.id);
        GetProductReviews(params.id);
    }, [params.id]);


    return (
        <div>
            {productData ? (
                <div>
                    <p><img src={productData.photoUrl} alt={`${productData.name} image`}/></p>
                    <p>name: {productData.name}</p>
                    <p>description: {productData.description}</p>
                    <p>price: {productData.price.amount}</p>
                    <p>currency: {productData.price.currency}</p>
                    <p>categories:</p>
                    <ul role="list">
                        {Object.entries(productData.categories).map(([key, value]) => (
                            <li key={key}>
                                {key}: {value}
                            </li>
                        ))}
                    </ul>
                </div>
            ) : (
                "Getting Product..."
            )}

            <Link href={`/cart/${params.id}`}><button>Add To Cart</button></Link>
            <br/>
            <br/>
            <h2>Reviews:</h2>
            {reviewsData ? (
                reviewsData.length > 0 ? (
                    <ul role="list">
                        {reviewsData.map(review => (
                            <li key={review.id}>
                                <h3>Title: {review.title}</h3>
                                <p>UserId: {review.userId}</p>
                                <p>Score: {review.score}</p>
                                <p>Description: {review.description}</p>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p>No reviews available for this product.</p>
                )
            ) : (
                "Getting Reviews..."
            )}
        </div>
    );
}