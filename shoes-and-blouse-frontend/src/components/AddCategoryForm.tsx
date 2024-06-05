import React, { useState, FormEvent } from 'react';
import { TextField, Button } from '@mui/material';
import { AddCategoryFormProps, Category } from '@/app/_types/api_interfaces';

const AddCategoryForm: React.FC<AddCategoryFormProps> = ({ products }) => {
    const [name, setName] = useState('');
    const [productNames, setProductNames] = useState<string>('');

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        const productArray = productNames.split(',').map(name => name.trim());
        const selectedProducts: Record<number, string> = {};

        productArray.forEach(name => {
            const product = products.find(p => p.name.toLowerCase() === name.toLowerCase());
            if (product) {
                selectedProducts[product.id] = product.name;
            }
        });

        const category: Category = { id: Date.now(), name, products: selectedProducts };
        console.log(category);
        // Możesz tutaj dodać zapisywanie kategorii do bazy danych lub wysyłanie do API
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                label="Nazwa"
                value={name}
                onChange={(e) => setName(e.target.value)}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Produkty (oddzielone przecinkami)"
                value={productNames}
                onChange={(e) => setProductNames(e.target.value)}
                fullWidth
                margin="normal"
            />
            <Button type="submit" variant="contained" color="primary" fullWidth>
                Dodaj Kategorię
            </Button>
        </form>
    );
};

export default AddCategoryForm;
