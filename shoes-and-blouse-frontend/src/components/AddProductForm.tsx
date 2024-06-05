"use client";

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import axios from "axios";
import { AddProductFormProps, Category } from "@/app/_types/api_interfaces";
import { GetCategories } from "@/app/actions/actions";
import { TextField, Checkbox, FormControlLabel, Button, Box, FormGroup, FormControl, FormLabel } from "@mui/material";

export default function AddProductForm() {
    const { register, handleSubmit, formState: { errors } } = useForm<AddProductFormProps>({});
    const [categories, setCategories] = useState<Category[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<number[]>([]);

    useEffect(() => {
        async function fetchCategories() {
            const categories = await GetCategories();
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

    const onSubmit = async (data: AddProductFormProps) => {
        const productData = { ...data, categories: selectedCategories };
        // Logging for debugging
        console.log('Form data:', data);
        console.log('Selected categories:', selectedCategories);
        console.log('Product data to be submitted:', productData);

        try {
            const response = await axios.post("/backendAPI/api/v1/Product/Create", productData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            })
                .then(response => console.log(response.data));

        } catch (error) {
            console.error('Error creating product:', (error as Error).message);
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
                    {...register("name", { required: "Name is required" })}
                    placeholder="Name of a product"
                    error={!!errors.name}
                    helperText={errors.name ? errors.name.message : ''}
                />

                <TextField
                    label="Description"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("description", { required: "Description is required" })}
                    placeholder="Description of the product"
                    error={!!errors.description}
                    helperText={errors.description ? errors.description.message : ''}
                />

                <TextField
                    label="Currency"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("price.currency", { required: "Currency is required" })}
                    placeholder="PLN"
                    error={!!errors.price?.currency}
                    helperText={errors.price?.currency ? errors.price.currency.message : ''}
                />

                <TextField
                    label="Amount"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    type="number"
                    {...register("price.amount", { required: "Amount is required" })}
                    placeholder="100"
                    error={!!errors.price?.amount}
                    helperText={errors.price?.amount ? errors.price.amount.message : ''}
                />

                <Box sx={{ display: 'flex' }}>
                    <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                        <FormLabel component="legend">Select Categories</FormLabel>
                        <FormGroup>
                            {categories.map(category => (
                                <FormControlLabel
                                    key={category.id}
                                    control={
                                        <Checkbox
                                            value={category.id.toString()}
                                            checked={selectedCategories.includes(category.id)}
                                            onChange={handleCheckboxChange}
                                            name={category.name}
                                        />
                                    }
                                    label={category.name}
                                />
                            ))}
                        </FormGroup>
                    </FormControl>
                </Box>
                <Button type="submit" variant="contained" color="primary" fullWidth>
                    Submit
                </Button>
            </form>
        </Box>
    );
}
