"use client"

import React, { useEffect, useState } from "react";
import axios from "axios";
import Link from 'next/link';
import { Box, IconButton, List, ListItem, ListItemText, Typography } from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import { Product } from "@/app/_types/api_interfaces";

const ProductList = () => {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetchProducts();
    }, []);

    const fetchProducts = async () => {
        try {
            const response = await axios.get("/backendAPI/api/v1/Product/GetAll", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            setProducts(response.data);
        } catch (error) {
            console.error("Error fetching products:", error);
        }
    };

    const handleDelete = async (productId: number) => {
        try {
            await axios.delete(`/backendAPI/api/v1/Product/Delete/${productId}`, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            setProducts(products.filter(product => product.id !== productId));
        } catch (error) {
            console.error("Error deleting product:", error);
        }
    };

    return (
        <Box sx={{ maxWidth: 600, mx: "auto", mt: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Typography variant="h4" gutterBottom>Product List</Typography>
                <Link href="/adminPanel/products/add" passHref>
                    <IconButton color="primary">
                        <AddIcon />
                    </IconButton>
                </Link>
            </Box>
            <List>
                {products.map(product => (
                    <ListItem key={product.id} sx={{ display: 'flex', justifyContent: 'space-between' }}>
                        <ListItemText
                            primary={product.name}
                            secondary={product.description}
                        />
                        <Box>
                            <Link href={`/adminPanel/products/update/${product.id}`} passHref>
                                <IconButton color="primary">
                                    <EditIcon />
                                </IconButton>
                            </Link>
                            <IconButton onClick={() => handleDelete(product.id)} color="secondary">
                                <DeleteIcon />
                            </IconButton>
                        </Box>
                    </ListItem>
                ))}
            </List>
        </Box>
    );
};

export default ProductList;
