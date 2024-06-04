"use client"

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import axios from "axios";
import { AddProductFormProps, Category } from "@/app/_types/api_interfaces";
import { GetCategories } from "@/app/actions/actions";
import { TextField, Checkbox, FormControlLabel, Button, Typography, Box, FormGroup } from "@mui/material";

export default function AddProductForm() {
    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<AddProductFormProps>({});

    const [categories, setCategories] = useState<Category[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<number[]>([]);

    useEffect(() => {
        async function fetchCategories() {
            const categories = await GetCategories();
            console.log(categories);
            setCategories(categories);
        }
        fetchCategories();
    }, []);

    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const categoryId = parseInt(event.target.value, 10);
        setSelectedCategories(prevSelected =>
            event.target.checked
                ? [...prevSelected, categoryId]
                : prevSelected.filter(id => id !== categoryId)
        );
    };

    const onSubmit = async (data: any) => {
        try {
            const productData = { ...data, categories: selectedCategories };
            await axios.post("/backendAPI/api/v1/Product/Create", productData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            }).then(res => console.log(res.data));
        } catch (error) {
            console.error('Błąd przy tworzeniu produktu: ', (error as Error).message);
        }
    };

    return (
        <Box component="div" sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
            <form onSubmit={handleSubmit(onSubmit)} noValidate>
                <TextField
                    label="Name"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("name", { required: "Wrong name" })}
                    error={!!errors.name}
                    helperText={errors.name ? errors.name.message : ""}
                    placeholder="Name of a product"
                />

                <TextField
                    label="Description"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("description", { required: "Wrong description" })}
                    error={!!errors.description}
                    helperText={errors.description ? errors.description.message : ""}
                    placeholder="Description of the product"
                />

                <TextField
                    label="Currency"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("currency", { required: "Wrong currency" })}
                    error={!!errors.currency}
                    helperText={errors.currency ? errors.currency.message : ""}
                    placeholder="PLN"
                />

                <TextField
                    label="Amount"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("amount", { required: "Wrong amount" })}
                    error={!!errors.amount}
                    helperText={errors.amount ? errors.amount.message : ""}
                    placeholder="100"
                />

                <Typography variant="h6" component="h3" gutterBottom>
                    Categories
                </Typography>
                <FormGroup>
                    {categories.map(category => (
                        <FormControlLabel
                            key={category.id}
                            control={
                                <Checkbox
                                    value={category.id}
                                    onChange={handleCheckboxChange}
                                />
                            }
                            label={category.name}
                        />
                    ))}
                </FormGroup>
                <Button type="submit" variant="contained" color="primary" fullWidth>
                    Submit
                </Button>
            </form>
        </Box>
    );
}
