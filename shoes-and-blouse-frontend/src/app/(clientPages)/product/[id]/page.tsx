import axios from "axios";
import {Product} from "@/app/_types/api_interfaces";

//calls the .NET api for a specific product data
async function GetProduct(id: string) {
    try {
        const { data } = await axios.get<Product>(`${process.env.API_URL}/api/v1/product/GetById/${id}`);
        console.log(data);
        return data;
    }
    catch(error)
    {
        console.error(error);
    }
}

export default async function ProductPage({ params }: { params: { id: string } }) {
    const productData = await GetProduct(params.id);

    return (
        <div>
            <h1>Fetched Product:</h1>
            {
                productData ?
                    <div>
                        <p>id: {productData.id}</p>
                        <p>name: {productData.name}</p>
                        <p>description: {productData.description}</p>
                        <p>price: {productData.price.amount}</p>
                        <p>currency: {productData.price.currency}</p>
                        <p>categories:</p>
                        <ul role={"list"}>
                            {Object.entries(productData.categories).map(([key, value]) => (
                                <li key={key}>
                                    {key}: {value}
                                </li>
                            ))}
                        </ul>
                        <p><img src={productData.photoUrl}/></p>
                    </div>
                    :
                    "Getting Product..."
            }
        </div>
    );
}