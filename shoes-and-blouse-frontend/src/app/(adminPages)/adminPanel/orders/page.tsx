import React from 'react';
import {Container, Typography} from "@mui/material";
import OrderList from "@/components/OrderList";
export default function App() {
    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Order Managment
            </Typography>
            <OrderList/>
        </Container>
    );
}
