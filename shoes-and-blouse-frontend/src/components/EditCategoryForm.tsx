"use client"

import { GetProducts, GetCategoryById } from "@/app/actions/actions";
import React, { useEffect, useState } from "react";
import { Category, Product } from "@/app/_types/api_interfaces";
import Box from "@mui/material/Box";
import {useFieldArray, useForm, useWatch} from "react-hook-form";
import axios from "axios";
import { Button, Checkbox, FormControlLabel, FormGroup, FormLabel, TextField, Typography } from "@mui/material";
import FormControl from "@mui/material/FormControl";

type ProductIdField = {
    id: string;
}
interface EditCategoryFormProps {

    categoryId: number;
    name?: string;
    productIds?: ProductIdField[];
}

 const EditCategoryForm : React.FC<ProductIdField> = ({id}) =>  {
    const {
        register,
        handleSubmit,
        formState: { errors },
        control
    } = useForm<EditCategoryFormProps>({
        defaultValues: {
            productIds: []
        }
    });
    const {
        append,
        remove
    } = useFieldArray({
        control,
        name: "productIds"
    }) as {
        fields: ProductIdField[];
        append: (value: { id: string }) => void;
        remove: (index: number) => void;
    };
    const [products, setProducts] = useState<Product[]>([]);
    const [category, setCategory] = useState<Category>();
    useEffect(() => {
        async function fetchProducts() {
            const fetchedProducts = await GetProducts();
            setProducts(fetchedProducts);
        }

        fetchProducts();
    }, []);

    useEffect(() => {
        async function fetchCategory() {
            const fetchedCategory = await GetCategoryById(id);
            setCategory(fetchedCategory);
        }

        fetchCategory();
    }, [id]);
    const productIds = useWatch({
        control,
        name: "productIds",
        defaultValue: []
    });
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const productId = event.target.value;
        const isChecked = event.target.checked;

        if (isChecked){
            if (!productIds?.some((field: ProductIdField) => field.id === productId)){
                append({id: productId});
            }
        }
        else{
            const index = productIds?.findIndex((field: ProductIdField) => field.id === productId);
            if (index !== -1 && index !== undefined){
                remove(index);
            }
        }
    };
    const onSubmit = async (data: EditCategoryFormProps) => {
        try{
            const categoryData = {...data,
                categoryId: id,
                productIds: data?.productIds!==undefined ? data.productIds.map(product => product.id) : undefined};
            console.log('Form data: ', data);
            console.log('Category data to be submitted: ', categoryData);

            const response = await axios.patch(`/backendAPI/api/v1/Category/Update`, categoryData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
        }
        catch (error){
            if (axios.isAxiosError(error)){
                if (error.response){
                    console.error('Error response data:', error.response.data);
                    console.error('Error response status:', error.response.status);
                    console.error('Error response headers:', error.response.headers);
                }
                else if (error.request){
                    console.error('Error request:', error.request);
                }
                else{
                    console.error('Error message:', error.message);
                }
                console.error('Error config:', error.config);
            }
            else{
                console.error('Unexpected error: ', error);
            }
        }
    }
    return(
        <Box component="div" sx={{maxWidth: 400, mx: "auto", mt: 4}}>
            {category != undefined ?
                <form onSubmit={handleSubmit(onSubmit)} noValidate>
                    <TextField
                        label="Name"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...register("name", { required: "Name is required" })}
                        error={!!errors.name}
                        helperText={errors.name ? errors.name.message : ''}
                        value={category.name}
                    />
                    <Box sx={{ display: 'flex' }}>
                        <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                            <FormLabel component="legend">Select Categories</FormLabel>
                            <FormGroup>
                                {products.map(product => (
                                    <FormControlLabel
                                        key={product.id}
                                        control={
                                            <Checkbox
                                                value={product.id.toString()}
                                                checked={productIds?.some(field => field.id === product.id.toString())}
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
                : "Loading..."
            }
        </Box>
    );
}

export default EditCategoryForm;