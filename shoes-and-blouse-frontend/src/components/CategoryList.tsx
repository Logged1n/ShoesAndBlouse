"use client"

import React, { useEffect, useState } from "react";
import axios from "axios";
import Link from 'next/link';
import { Box, IconButton, List, ListItem, ListItemText, Typography } from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import { Category } from "@/app/_types/api_interfaces"; // assuming you have a Category type

const CategoryList = () => {
    const [categories, setCategories] = useState<Category[]>([]);

    useEffect(() => {
        fetchCategories();
    }, []);

    const fetchCategories = async () => {
        try {
            const response = await axios.get("/backendAPI/api/v1/Category/GetAll", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            setCategories(response.data);
        } catch (error) {
            console.error("Error fetching categories:", error);
        }
    };

    const handleDelete = async (categoryId: number) => {
        try {
            await axios.delete(`/backendAPI/api/v1/Category/Delete/${categoryId}`, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            setCategories(categories.filter(category => category.id !== categoryId));
        } catch (error) {
            console.error("Error deleting category:", error);
        }
    };

    return (
        <Box sx={{ maxWidth: 600, mx: "auto", mt: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Typography variant="h4" gutterBottom>Category List</Typography>
                <Link href="/adminPanel/categories/add" passHref>
                    <IconButton color="primary">
                        <AddIcon />
                    </IconButton>
                </Link>
            </Box>
            <List>
                {categories.map(category => (
                    <ListItem key={category.id} sx={{ display: 'flex', justifyContent: 'space-between' }}>
                        <ListItemText
                            primary={category.name}
                            secondary={category.description}
                        />
                        <Box>
                            <Link href={`/adminPanel/categories/update/${category.id}`} passHref>
                                <IconButton color="primary">
                                    <EditIcon />
                                </IconButton>
                            </Link>
                            <IconButton onClick={() => handleDelete(category.id)} color="secondary">
                                <DeleteIcon />
                            </IconButton>
                        </Box>
                    </ListItem>
                ))}
            </List>
        </Box>
    );
};

export default CategoryList;
