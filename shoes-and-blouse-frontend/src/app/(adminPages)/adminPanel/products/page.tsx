"use client"

import React from 'react';
import { Container, Typography } from '@mui/material';
import AddProductForm from '@/components/AddProductForm';

const AddProduct: React.FC = () => {
    const handleAddProduct = (product: any) => {
        // Logika zapisywania produktu
        console.log(product);
        // Możesz tutaj dodać zapisywanie produktu do bazy danych lub wysyłanie do API
    };

    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Dodaj Produkt
            </Typography>
            <AddProductForm onSubmit={handleAddProduct} />
        </Container>
    );
};

export default AddProduct;
