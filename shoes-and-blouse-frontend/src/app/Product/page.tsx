

export default function ProductPage() {

        const fetchData = async () => {
            try {
                const response = await fetch("/api/v1/products/1");
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }

                const data = await response.json();
                console.log(data);
            }
            catch (error) {
                console.error(error);
            }
        }
        const productInfo =  fetchData
        return (
          <div>{productInfo}</div>
        );
}