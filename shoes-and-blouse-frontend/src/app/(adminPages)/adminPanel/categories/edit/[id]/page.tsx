"use client"

import {Container, Typography} from "@mui/material";
import EditCategoryForm from "@/components/AddCategoryForm";
import React from "react";

export default function EditCategory({params}: {params: {id: string}}){
    return(
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Edit Category
            </Typography>
            <EditCategoryForm params={params}>

            </EditCategoryForm>
        </Container>
    )
}