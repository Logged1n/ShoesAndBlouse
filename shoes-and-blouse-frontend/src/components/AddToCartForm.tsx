"use client"

import React from "react";
import Box from "@mui/material/Box";
import { TextField, Button } from "@mui/material";
import { useForm } from "react-hook-form";
import axios from "axios";

type ProductIdField = {
    id: string;
}
interface AddToCartFormProps {
    productId: number;
    qty: number;
}
const AddToCartForm: React.FC<ProductIdField> = ({ id }) => {
    const {
        register,
        handleSubmit,
        formState: { errors },
        control
    } = useForm<AddToCartFormProps>({
        defaultValues: {
            qty: 0
        }
    });

    const onSubmit = async (data: AddToCartFormProps) => {
        try {
            const cartData = {
                ...data,
                productId: id,
            };
            console.log("Cart data:", cartData);
            const response = await axios.post('/backendAPI/api/v1/Cart/Add', cartData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*"
                }
            })
        } catch (error) {
            console.error("Error adding to cart:", error);
        }
    };

    return (
        <Box component="div" sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
            <form onSubmit={handleSubmit(onSubmit)} noValidate>
                <TextField
                    label="Quantity"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("qty", { required: true, min: 1 })}
                    error={!!errors.qty}
                    helperText={errors.qty ? "Quantity is required and must be at least 1" : ""}
                />
                <Button type="submit" variant="contained" color="primary">
                    Add to Cart
                </Button>
            </form>
        </Box>
    );
};
export default AddToCartForm;
