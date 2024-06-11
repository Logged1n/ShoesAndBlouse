"use client"
import React, { useEffect, useState } from 'react';
import styles from '../../../styles/page.module.css';
import { GetAllProducts } from '../../actions/actions';
import { Product } from '../../_types/api_interfaces'; // Importowanie typu Product

const ViewProductPage: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const data = await GetAllProducts();
                console.log('Pobrane produkty:', data);  // Dodane logowanie
                if (data.length === 0) {
                    setError('Brak produktów do wyświetlenia.');
                }
                setProducts(data);
            } catch (err) {
                setError('Błąd przy pobieraniu produktów.');
                console.error('Fetch error:', err); // Dodane logowanie błędu
            } finally {
                setLoading(false);
            }
        };

        fetchProducts();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className={styles.container}>
            <h1>Produkty</h1>
            {products.length === 0 ? (
                <p>Brak produktów do wyświetlenia.</p>
            ) : (
                <div className={styles.productGrid}>
                    {products.map(product => (
                        <div key={product.id} className={styles.productItem}>
                            <img src={product.photoUrl} alt={product.name} />
                            <h3>{product.name}</h3>
                            <p>{product.description}</p>
                            <p className={styles.price}>{product.price.amount} {product.price.currency}</p>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default ViewProductPage;
