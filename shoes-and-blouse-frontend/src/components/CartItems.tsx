"use client"

import {useEffect, useState} from "react";
import axios from "axios";
import {List, ListItem, ListItemText, Typography} from "@mui/material";
import {Cart, cartItem} from "@/app/_types/api_interfaces";
import Box from "@mui/material/Box";

const CartItems = () => {
    const [cart, setCart] = useState<Cart | null>(null);

    useEffect(() => {
        fetchItems();
    }, []);

    const fetchItems = async () => {
        try {
            const response = await axios.get('/backendAPI/api/v1/Cart/Get', {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            const data = response.data;
            setCart(data);
        } catch (error) {
            console.error("Error fetching cart items", error);
        }
    };

    return (
        <Box>
            {cart ? (
                <>
                    <List>
                        {cart.cartItems.map((item: cartItem) => (
                            <ListItem key={item.productId}>
                                <ListItemText
                                    primary={`Product ID: ${item.productId}`}
                                    secondary={`Quantity: ${item.qty}`}
                                />
                            </ListItem>
                        ))}
                    </List>
                    <Typography variant="h6">
                        Total: {cart.total.amount} {cart.total.currency}
                    </Typography>
                </>
            ) : (
                <Typography variant="h6">Loading...</Typography>
            )}
        </Box>
    );
};

export default CartItems;
