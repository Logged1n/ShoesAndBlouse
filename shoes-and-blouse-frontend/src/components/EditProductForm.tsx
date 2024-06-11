"use client"

import {GetCategories, GetProductById} from "@/app/actions/actions";
import React, {useEffect, useState} from "react";
import {Category, Price, Product} from "@/app/_types/api_interfaces";
import Box from "@mui/material/Box";
import {useFieldArray, useForm, useWatch} from "react-hook-form";
import axios from "axios";
import {Button, Checkbox, FormControlLabel, FormGroup, FormLabel, TextField, Typography} from "@mui/material";
import FormControl from "@mui/material/FormControl";

type CategoryIdField = {
    id: string;
}
interface EditProductFormProps {
    name: string;
    description: string;
    price: Price;
    categoryIds: CategoryIdField[];
    PhotoUrl: string;
}

export default function EditProductForm ({ params }: { params: { id: string } }) {
    const { register,
        handleSubmit,
        formState: { errors },
        control } = useForm<EditProductFormProps>({
        defaultValues: {
            categoryIds: []
        }
    });
    const {
        append,
        remove } = useFieldArray({
        control,
        name: "categoryIds"
    }) as {
        fields: CategoryIdField[];
        append: (value: { id: string }) => void;
        remove: (index: number) => void;
    };
    const [categories, setCategories] = useState<Category[]>([]);
    const { setValue } = useForm();
    const [product, setProduct] = useState<Product>();
    useEffect(() => {

            async function fetchProduct(){
                const fetchedProduct = await GetProductById(params.id);
                setProduct(fetchedProduct);
            }
            fetchProduct();
        },
        [params.id]);
    useEffect(()=> {
            async function fetchCategories() {
                const categories = await GetCategories();
                setCategories(categories);
            }
            fetchCategories();
        },
        []);
    const categoryIds = useWatch({
        control,
        name: "categoryIds",
        defaultValue: []
    });
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const categoryId = event.target.value;
        const isChecked = event.target.checked;

        if (isChecked) {
            if (!categoryIds.some((field: CategoryIdField)  => field.id === categoryId)) {
                append({ id: categoryId });
            }
        } else {
            const index = categoryIds.findIndex((field: CategoryIdField) => field.id === categoryId);
            if (index !== -1) {
                remove(index);
            }
        }
    };
    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files[0]) {
            setValue('image', event.target.files[0]);
        }
    };
    const onSubmit = async (data: EditProductFormProps) => {
        try{
            const productData = {...data, categoryIds: data.categoryIds.map(category => category.id)};
            console.log('Form data: ',data);
            console.log('Product data to be submitted: ', productData);

            const response = await axios.patch(`/backendAPI/api/v1/Product/Update${params.id}`, productData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
        }
        catch (error){
            if (axios.isAxiosError(error)) {
                if (error.response) {
                    console.error('Error response data:', error.response.data);
                    console.error('Error response status:', error.response.status);
                    console.error('Error response headers:', error.response.headers);
                } else if (error.request) {
                    console.error('Error request:', error.request);
                } else {
                    console.error('Error message:', error.message);
                }
                console.error('Error config:', error.config);
            } else {
                console.error('Unexpected error:', error);
            }
        }
    }
    return (

        <Box component="div" sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
            {product != undefined ?
                <form onSubmit={handleSubmit(onSubmit)} noValidate>
                    <TextField
                        label="Name"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...register("name", { required: "Name is required" })}
                        error={!!errors.name}
                        helperText={errors.name ? errors.name.message : ''}
                    />
                    <TextField
                        label="Description"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...register("description", { required: "Description is required" })}
                        error={!!errors.description}
                        helperText={errors.description ? errors.description.message : ''}
                    />
                    <TextField
                        label="Currency"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...register("price.currency", { required: "Currency is required" })}
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
                                                checked={categoryIds.some(field => field.id === category.id.toString())}
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
                    <Box sx={{ my: 2 }}>
                        <input
                            type="file"
                            accept="image/*"
                            onChange={handleFileChange}
                        />
                    </Box>
                    <Button type="submit" variant="contained" color="primary" fullWidth>
                        Submit
                    </Button>
                </form>
                : "Loading..."}
        </Box>
    );

}