"use client"

import {Container, Typography} from "@mui/material"
import CartItems from "@/components/CartItems";
import React from "react";
import Link from "next/link";

export default function CarItems(){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Cart Items
            </Typography>
            <CartItems/>
            <Link href={"/order"}>
                <button>Make Order</button>
            </Link>
        </Container>
    )
}