"use client"
import React from 'react';
import { Container, Typography } from '@mui/material';
import AddCategoryForm from '@/components/AddCategoryForm';
import { Product } from '@/app/_types/api_interfaces';

const AddCategory: React.FC = () => {
    const products: Product[] = [
        { id: 1, name: 'Laptop', description: '', price: { currency: 'USD', amount: 1000 }, categories: {}, photoUrl: '' },
        { id: 2, name: 'Koszulka', description: '', price: { currency: 'USD', amount: 20 }, categories: {}, photoUrl: '' },
        { id: 3, name: 'Krzesło', description: '', price: { currency: 'USD', amount: 50 }, categories: {}, photoUrl: '' },
    ];

    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Dodaj Kategorię
            </Typography>
            <AddCategoryForm products={products} />
        </Container>
    );
};

export default AddCategory;