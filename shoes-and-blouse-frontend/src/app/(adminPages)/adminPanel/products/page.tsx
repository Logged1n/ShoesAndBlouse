import React from 'react';
import ProductList from '@/components/ProductList';
import {Container, Typography} from "@mui/material";


export default function App() {
    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Product Managment
            </Typography>
            <ProductList/>
        </Container>
    );
}
