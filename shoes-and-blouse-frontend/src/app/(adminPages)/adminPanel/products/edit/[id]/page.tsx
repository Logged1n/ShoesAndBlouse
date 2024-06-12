"use client"

import {Container, Typography} from "@mui/material";
import EditProductForm from "@/components/EditProductForm";
import React from "react";

export default function EditProduct({params}: {params: {id: string}}){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Edit Product
            </Typography>
            <EditProductForm id={params.id as string}/>
        </Container>
    )
}
