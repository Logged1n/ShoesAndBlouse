"use client"

import React from 'react';
import { Container, Typography } from '@mui/material';
import AddProductForm from '@/components/AddProductForm';

const AddProduct: React.FC = () => {

    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Add Product
            </Typography>
            <AddProductForm />
        </Container>
    );
};

export default AddProduct;
