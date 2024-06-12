"use client"

import {Container, Typography} from "@mui/material";
import AddToCartForm from "@/components/AddToCartForm";
import React from "react";

export default function AddToCart({params}: {params: {id: string}}){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Add to Cart
            </Typography>
            <AddToCartForm id={params.id as string}/>
        </Container>
    )
}