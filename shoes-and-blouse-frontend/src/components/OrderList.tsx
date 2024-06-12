"use client"

import {useEffect, useState} from "react";
import {OrderForm} from "@/app/_types/api_interfaces";
import axios from "axios";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {List, ListItem, ListItemText} from "@mui/material";
import Link from "next/link";
import IconButton from "@mui/material/IconButton";
import EditIcon from "@mui/icons-material/Edit";


const OrderList = () => {
    const OrderStatus = ["Open", "Confirmed", "Shipped", "Arrived", "Collected", "Completed", "Returning", "Cancelled"];

    const [orders, setOrders] = useState<OrderForm[]>([])
    useEffect(() => {
        fetchOrders();
    }, []);

    const fetchOrders = async () => {
        try{
            const response = await axios.get('/backendAPI/api/v1/Order/GetAllNotCompleted', {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            console.log(response);
            setOrders(response.data);
        }
        catch (error){
            console.error("Error fetching orders", error);
        }
    };
    return (
        <Box sx={{maxWidth: 600, mx: "auto", mt: 4}}>
            <Box sx={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
                <Typography variant={"h4"} gutterBottom>
                    Order List
                </Typography>
            </Box>
            <List>
                {orders.map(order => (
                    <ListItem key={order.id} sx={{display: 'flex', justifyContent:'space-between'}}>
                        <ListItemText
                            primary={order.id}
                            secondary={OrderStatus[order.status]}
                        />
                        <Box>
                            <Link href={`/adminPanel/orders/${order.id}`} passHref>
                                <IconButton color="primary">
                                    <EditIcon/>
                                </IconButton>
                            </Link>
                        </Box>
                    </ListItem>
                ))}
            </List>
        </Box>
    );
};
export default OrderList;