"use client"

import React from 'react';
import {Container, Typography} from "@mui/material";
import AddCategoryForm from "@/components/AddCategoryForm";

const AddCategory: React.FC = () => {
    return (
       <Container>
           <Typography variant="h4" align="center" gutterBottom>
               Add Category
           </Typography>
           <AddCategoryForm/>
       </Container>
    );
}
export default AddCategory;