import React, { useState, ChangeEvent, FormEvent } from 'react';
import { TextField, Button, FormControl } from '@mui/material';
import { Price, Category } from '@/app/_types/api_interfaces';
import { AddProductFormProps } from '@/app/_types/api_interfaces';

const AddProductForm: React.FC<AddProductFormProps> = ({ onSubmit }) => {
    const [name, setName] = useState('');
    const [price, setPrice] = useState<Price>({ currency: 'PLN', amount: 0 });
    const [description, setDescription] = useState('');
    const [image, setImage] = useState('');
    const [categories, setCategories] = useState<string>('');

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        const categoryArray = categories.split(',').map(category => category.trim());
        const product = { name, price, description, image, categories: categoryArray };
        onSubmit(product);
    };

    const handlePriceChange = (e: ChangeEvent<HTMLInputElement | { name?: string; value: unknown }>) => {
        setPrice({ ...price, [e.target.name as string]: e.target.value as string | number });
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField label="Nazwa" value={name} onChange={(e) => setName(e.target.value)} fullWidth margin="normal" />
            <TextField
                label="Kwota"
                type="number"
                name="amount"
                value={price.amount}
                onChange={handlePriceChange}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Waluta"
                name="currency"
                value={price.currency}
                onChange={handlePriceChange}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Opis"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                fullWidth
                margin="normal"
                multiline
                rows={4}
            />
            <TextField label="Zdjęcie URL" value={image} onChange={(e) => setImage(e.target.value)} fullWidth margin="normal" />
            <TextField
                label="Kategorie (oddzielone przecinkami)"
                value={categories}
                onChange={(e) => setCategories(e.target.value)}
                fullWidth
                margin="normal"
            />
            <Button type="submit" variant="contained" color="primary" fullWidth>
                Dodaj Produkt
            </Button>
        </form>
    );
};

export default AddProductForm;
