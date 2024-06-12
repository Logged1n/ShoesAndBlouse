"use client"

import {Container, Typography} from "@mui/material";
import EditOrder from "@/components/EditOrderStatus";
import React from "react";

export default function OrderStatus({params}: {params: {id: string}}){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Edit Order Status
            </Typography>
            <EditOrder id={params.id as string}/>
        </Container>
    )
}