"use client"

import React, { useEffect, useState } from 'react';
import styles from '@/styles/page.module.css';
import { GetProducts } from '@/app/actions/actions';
import { Product } from '@/app/_types/api_interfaces';
import { AddShoppingCart } from "@mui/icons-material";
import Link from 'next/link';

const ViewProductPage: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [filteredProducts, setFilteredProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [nameFilter, setNameFilter] = useState<string>('');
    const [categoryFilter, setCategoryFilter] = useState<string>('');

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const data = await GetProducts();
                console.log('Pobrane produkty:', data);
                if (data.length === 0) {
                    setError('Brak produktów do wyświetlenia.');
                }
                setProducts(data);
                setFilteredProducts(data);
            } catch (err) {
                setError('Błąd przy pobieraniu produktów.');
                console.error('Fetch error:', err);
            } finally {
                setLoading(false);
            }
        };

        fetchProducts();
    }, []);

    useEffect(() => {
        filterProducts();
    }, [nameFilter, categoryFilter, products]);

    const filterProducts = () => {
        let filtered = products;
        if (nameFilter) {
            filtered = filtered.filter(product =>
                product.name.toLowerCase().includes(nameFilter.toLowerCase())
            );
        }
        if (categoryFilter) {
            filtered = filtered.filter(product =>
                Object.values(product.categories).some(category =>
                    category.toLowerCase().includes(categoryFilter.toLowerCase())
                )
            );
        }
        setFilteredProducts(filtered);
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className={styles.container}>
            <h1>Produkty</h1>
            <div className={styles.filters}>
                <input
                    type="text"
                    placeholder="Filtruj po nazwie"
                    value={nameFilter}
                    onChange={e => setNameFilter(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Filtruj po kategorii"
                    value={categoryFilter}
                    onChange={e => setCategoryFilter(e.target.value)}
                />
            </div>
            {filteredProducts.length === 0 ? (
                <p>Brak produktów do wyświetlenia.</p>
            ) : (
                <div className={styles.productGrid}>
                    {filteredProducts.map(product => (
                        <div key={product.id} className={styles.productItem}>
                            <Link href={`/product/${product.id}`}>
                                <img src={product.photoUrl} alt={product.name} />
                            </Link>
                            <div style={{ display: "flex", alignItems: "center" }}>
                                <h3>{product.name}</h3>
                                <Link href={`/cart/${product.id}`}>
                                    <AddShoppingCart style={{ cursor: 'pointer', marginLeft: '10px' }} />
                                </Link>
                            </div>
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
