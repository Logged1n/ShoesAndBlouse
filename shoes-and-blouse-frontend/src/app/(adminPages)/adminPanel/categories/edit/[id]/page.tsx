"use client"

import {Container, Typography} from "@mui/material";
import EditCategoryForm from "@/components/EditCategoryForm";
import React from "react";


export default function EditCategory({params}: {params: {id: string}}){

    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Edit Category
            </Typography>
            <EditCategoryForm id={params.id as string}/>
        </Container>
    )
}