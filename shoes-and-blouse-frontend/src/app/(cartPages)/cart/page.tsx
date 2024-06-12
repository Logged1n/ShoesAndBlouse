"use client"

import {Container, Typography} from "@mui/material"
import CartItems from "@/components/CartItems";
import React from "react";

export default function CarItems(){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Cart Items
            </Typography>
            <CartItems/>
        </Container>
    )
}