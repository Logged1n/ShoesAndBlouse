"use client"

import React, {useEffect, useState} from "react";
import {useForm, useFieldArray, useWatch} from "react-hook-form";
import axios from "axios";
import {Category, Product} from "@/app/_types/api_interfaces";
import {GetProducts} from "@/app/actions/actions";
import Box from "@mui/material/Box";
import {Button, Checkbox, FormControl, FormControlLabel, FormGroup, FormLabel, TextField} from "@mui/material";

type ProductIdField = {
    id: string;
}
interface AddCategoryFormProps {
    name:string;
    description: string;
    productIds: ProductIdField[];
}

export default function AddCategoryForm(){
    const {register,
        handleSubmit,
        formState: {errors},
        control } = useForm<AddCategoryFormProps>({
        defaultValues: {
            productIds: []
        }
    });
    const {
        append,
        remove } = useFieldArray({
        control,
        name: "productIds"
    }) as {
        fields: ProductIdField[];
        append: (value: {id: string}) => void;
        remove: (index: number) => void;
    };
    const [products, setProducts] = useState<Product[]>([]);
    useEffect(() => {
        async function fetchProducts(){
            const products = await GetProducts();
            setProducts(products);
        }
        fetchProducts();
    },
        []);
    const productIds = useWatch({
        control,
        name: "productIds",
        defaultValue: []
    });
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const productId = event.target.value;
        const isChecked = event.target.checked;

        if (isChecked){
            if (!productIds.some((field: ProductIdField) => field.id === productId)){
                append({ id: productId});
            }
        }
        else {
            const index = productIds.findIndex((field: ProductIdField) => field.id === productId);
            if (index !== -1) {
                remove(index);
            }
        }
    }
    const onSubmit = (data: AddCategoryFormProps) => {
        const categoryData = {...data, productIds: data.productIds.map(product => product.id)};
        console.log('Form data: ', data);
        console.log('Category data to be submitted: ', categoryData);

        axios.post("/backendAPI/api/v1/Category/Create", categoryData, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "*/*",
            }
        })
    }
    return (
        <Box component="div" sx={{maxWidth:400, mx: "auto", mt: 4}}>
            <form onSubmit={handleSubmit(onSubmit)} noValidate>
                <TextField
                    label="Name"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("name", { required: "Name is required" })}
                    placeholder="Name of a category"
                    error={!!errors.name}
                    helperText={errors.name ? errors.name.message : ''}
                />
                <TextField
                    label="Description"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register("description", { required: "Description is required" })}
                    placeholder="Descritpion of a category"
                    error={!!errors.description}
                    helperText={errors.description ? errors.description.message : ''}
                />
                <Box sx={{ display: 'flex' }}>
                    <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                        <FormLabel component="legend">Select Products</FormLabel>
                        <FormGroup>
                            {products.map(product => (
                                <FormControlLabel
                                    key={product.id}
                                    control={
                                        <Checkbox
                                            value={product.id.toString()}
                                            checked={productIds.some(field => field.id === product.id.toString())}
                                            onChange={handleCheckboxChange}
                                            name={product.name}
                                        />
                                    }
                                    label={product.name}
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

