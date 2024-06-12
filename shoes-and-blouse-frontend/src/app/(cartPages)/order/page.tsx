"use client"

import React from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import axios from 'axios';

interface Address {
    line1: string;
    line2?: string;
    city: string;
    postalCode: string;
    country: string;
}

interface FormInputs {
    billingAddress: Address;
    shippingAddress?: Address;
}

const AddressForm: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<FormInputs>();

    const onSubmit: SubmitHandler<FormInputs> = async (data) => {
        try {
            const response = await axios.post('/backendAPI/api/v1/Order/MakeOrder', data);
            console.log('Response:', response.data);
        } catch (error) {
            console.error('Error submitting form:', error);
        }
    };

    return (
        <div>
            <h1>Enter Addresses</h1>
            <form onSubmit={handleSubmit(onSubmit)}>
                <h2>Billing Address</h2>
                <div>
                    <label htmlFor="billingAddress.line1">Line 1</label>
                    <input
                        id="billingAddress.line1"
                        {...register("billingAddress.line1", { required: true })}
                    />
                    {errors.billingAddress?.line1 && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="billingAddress.line2">Line 2</label>
                    <input id="billingAddress.line2" {...register("billingAddress.line2")} />
                </div>
                <div>
                    <label htmlFor="billingAddress.city">City</label>
                    <input
                        id="billingAddress.city"
                        {...register("billingAddress.city", { required: true })}
                    />
                    {errors.billingAddress?.city && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="billingAddress.postalCode">Postal Code</label>
                    <input
                        id="billingAddress.postalCode"
                        {...register("billingAddress.postalCode", { required: true })}
                    />
                    {errors.billingAddress?.postalCode && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="billingAddress.country">Country</label>
                    <input
                        id="billingAddress.country"
                        {...register("billingAddress.country", { required: true })}
                    />
                    {errors.billingAddress?.country && <span>This field is required</span>}
                </div>

                <h2>Shipping Address</h2>
                <div>
                    <label htmlFor="shippingAddress.line1">Line 1</label>
                    <input
                        id="shippingAddress.line1"
                        {...register("shippingAddress.line1", { required: false })}
                    />
                    {errors.shippingAddress?.line1 && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="shippingAddress.line2">Line 2</label>
                    <input id="shippingAddress.line2" {...register("shippingAddress.line2")} />
                </div>
                <div>
                    <label htmlFor="shippingAddress.city">City</label>
                    <input
                        id="shippingAddress.city"
                        {...register("shippingAddress.city", { required: false })}
                    />
                    {errors.shippingAddress?.city && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="shippingAddress.postalCode">Postal Code</label>
                    <input
                        id="shippingAddress.postalCode"
                        {...register("shippingAddress.postalCode", { required: false })}
                    />
                    {errors.shippingAddress?.postalCode && <span>This field is required</span>}
                </div>
                <div>
                    <label htmlFor="shippingAddress.country">Country</label>
                    <input
                        id="shippingAddress.country"
                        {...register("shippingAddress.country", { required: false })}
                    />
                    {errors.shippingAddress?.country && <span>This field is required</span>}
                </div>

                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default AddressForm;
