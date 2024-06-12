"use client"

import {useEffect, useState} from "react";
import axios from "axios";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import MenuItem from "@mui/material/MenuItem";
import {useForm, Controller} from "react-hook-form";
import {OrderForm} from "@/app/_types/api_interfaces";

interface EditOrderFormProps {
    orderId: string;
    status?: number;
}
const EditOrder: React.FC<{ id: string }> = ({ id }) => {
    const OrderStatus = ["Open", "Confirmed", "Shipped", "Arrived", "Collected", "Completed", "Returning", "Cancelled"];

    const { control, handleSubmit, formState: { errors }, setValue } = useForm<EditOrderFormProps>();

    const [order, setOrder] = useState<OrderForm | null>(null);

    useEffect(() => {
        if (id) {
            fetchOrder(id);
        }
    }, [id]);

    const fetchOrder = async (orderId: string) => {
        try {
            const response = await axios.get(`/backendAPI/api/v1/Order/GetById/${orderId}`, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
            setOrder(response.data);
            setValue("status", Number(OrderStatus[response.data.status]));
        } catch (error) {
            console.error("Error fetching order", error);
        }
    };

    const onSubmit = async (data: EditOrderFormProps) => {
        try {
            const orderData = {
                orderId: id,
                status: OrderStatus.indexOf(data.status!)
            };
            console.log('Form data: ', data);
            console.log('Order data to be submitted: ', orderData);

            const response = await axios.patch(`/backendAPI/api/v1/Order/UpdateStatus`, orderData, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                }
            });
        } catch (error) {
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
                console.error('Unexpected error: ', error);
            }
        }
    };

    return (
        <Box component="div" sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
            {order ? (
                <form onSubmit={handleSubmit(onSubmit)} noValidate>
                    <Controller
                        name="status"
                        control={control}
                        defaultValue=""
                        render={({ field }) => (
                            <TextField
                                select
                                label="Order Status"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                {...field}
                                error={!!errors.status}
                                helperText={errors.status ? errors.status.message : ''}
                            >
                                {OrderStatus.map((statusOption, index) => (
                                    <MenuItem key={index} value={statusOption}>
                                        {statusOption}
                                    </MenuItem>
                                ))}
                            </TextField>
                        )}
                    />
                    <Button type="submit" variant="contained" color="primary" fullWidth>
                        Update Status
                    </Button>
                </form>
            ) : (
                <Typography variant="h6">Loading...</Typography>
            )}
        </Box>
    );
};

export default EditOrder;
