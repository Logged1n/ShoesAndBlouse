"use client"

import React, { useEffect, useState } from 'react';
import styles from '@/styles/page.module.css';
import { GetProducts } from '@/app/actions/actions';
import { Product } from '@/app/_types/api_interfaces';
import { AddShoppingCart } from "@mui/icons-material";
import Link from 'next/link';
import styles from '../../../styles/page.module.css';
import { GetProducts } from '../../actions/actions';
import { Product } from '../../_types/api_interfaces'; // Importowanie typu Product

const ViewProductPage: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [filterText, setFilterText] = useState<string>(''); // State for filter text
    const [selectedCategory, setSelectedCategory] = useState<string>(''); // State for selected category

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const data = await GetProducts();
                console.log('Pobrane produkty:', data);
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

    const handleFilterChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFilterText(event.target.value);
    };

    const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedCategory(event.target.value);
    };

    const getUniqueCategories = () => {
        const categories = new Set<string>();
        products.forEach(product => {
            Object.values(product.categories).forEach(category => categories.add(category));
        });
        return Array.from(categories);
    };

    const filteredProducts = products.filter(product =>
        product.name.toLowerCase().includes(filterText.toLowerCase()) &&
        (selectedCategory === '' || Object.values(product.categories).includes(selectedCategory))
    );

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className={styles.container}>
            <h1>Produkty</h1>
            <input
                type="text"
                placeholder="Szukaj produktów..."
                value={filterText}
                onChange={handleFilterChange}
                className={styles.searchInput}
            />
            <select value={selectedCategory} onChange={handleCategoryChange} className={styles.categorySelect}>
                <option value="">Wszystkie kategorie</option>
                {getUniqueCategories().map(category => (
                    <option key={category} value={category}>{category}</option>
                ))}
            </select>
            {filteredProducts.length === 0 ? (
                <p>Brak produktów do wyświetlenia.</p>
            ) : (
                <div className={styles.productGrid}>
                    {filteredProducts.map(product => (
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
