import axios from "axios";

//calls the .NET api for a specific product data
async function GetProduct(id: string) {
    try {
        const { data } = await axios.get(`${process.env.API_URL}/api/v1/product/GetById/${id}`);
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
                        <p>category: {productData.categories[0]}</p>
                        <p>{productData.photoPath}</p>
                    </div>
                    :
                    "Getting Product..."
            }
        </div>
    );
}