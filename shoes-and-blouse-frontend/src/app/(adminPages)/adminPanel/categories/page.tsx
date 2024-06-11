import React from 'react';
import {Container, Typography} from "@mui/material";
import CategoryList from "@/components/CategoryList";


export default function App() {
    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Category Managment
            </Typography>
            <CategoryList/>
        </Container>
    );
}
