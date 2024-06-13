"use client";
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useForm, SubmitHandler } from 'react-hook-form';

type Address = {
    line1: string;
    line2: string;
    city: string;
    country: string;
    postalCode: string;
}

interface UserForm {
    id: number;
    name?: string;
    email?: string;
    surname?: string;
    address?: Address;
}

const UserProfile: React.FC = () => {
    const [userId, setUserId] = useState<string>("0");
    const [user, setUser] = useState<UserForm | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    const { register, handleSubmit, formState: { errors } } = useForm<UserForm>();

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const { data } = await axios.get<UserForm>(`/backendAPI/api/v1/User/GetById/${userId}`);
                setUser(data);
                setLoading(false);
            } catch (err) {
                setError('Błąd przy pobieraniu użytkownika');
                setLoading(false);
            }
        };

        const fetchUserId = async () => {
            try {
                const { data } = await axios.get<string>(`/backendAPI/api/v1/User/GetId`)
                setUserId(data);
            }
            catch (err)
            {
                setError("błąd");
                setLoading(false);
            }
        }
        fetchUserId();
        fetchUser();
    }, [userId]);

    const onSubmit: SubmitHandler<UserForm> = async (data) => {
        try {
            await axios.patch(`/backendAPI/api/v1/User/Update/`, { ...data, id: user?.id, email: user?.email });
            setUser(prevUser => prevUser ? { ...prevUser, ...data } : null);
        } catch (err) {
            console.error('Update user error:', err);
            setError('Błąd przy aktualizacji użytkownika');
        }
    };

    if (loading) {
        return <div>Ładowanie...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            {user ? (
                <div>
                    <h1>{user.name} {user.surname}</h1>
                    <p>Email: {user.email}</p>
                    <div>
                        <p>Address:</p>
                        <p>{user.address?.line1}</p>
                        <p>{user.address?.line2}</p>
                        <p>{user.address?.city}</p>
                        <p>{user.address?.country}</p>
                        <p>{user.address?.postalCode}</p>
                    </div>
                </div>
            ) : (
                <div>Nie znaleziono użytkownika</div>
            )}
            <div>
                <h2>Zaaktualizuj swoje dane:</h2>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div>
                        <label htmlFor="name">Name:</label>
                        <input
                            id="name"
                            defaultValue={user?.name}
                            {...register("name", { required: true })}
                        />
                        {errors.name && <span>To pole jest wymagane</span>}
                    </div>
                    <div>
                        <label htmlFor="surname">Surname:</label>
                        <input
                            id="surname"
                            defaultValue={user?.surname}
                            {...register("surname", { required: true })}
                        />
                        {errors.surname && <span>To pole jest wymagane</span>}
                    </div>
                    <div>
                        <label htmlFor="line1">Address Line 1:</label>
                        <input
                            id="line1"
                            defaultValue={user?.address?.line1}
                            {...register("address.line1", { required: true })}
                        />
                        {errors.address?.line1 && <span>To pole jest wymagane</span>}
                    </div>
                    <div>
                        <label htmlFor="line2">Address Line 2:</label>
                        <input
                            id="line2"
                            defaultValue={user?.address?.line2}
                            {...register("address.line2")}
                        />
                    </div>
                    <div>
                        <label htmlFor="city">City:</label>
                        <input
                            id="city"
                            defaultValue={user?.address?.city}
                            {...register("address.city", { required: true })}
                        />
                        {errors.address?.city && <span>To pole jest wymagane</span>}
                    </div>
                    <div>
                        <label htmlFor="country">Country:</label>
                        <input
                            id="country"
                            defaultValue={user?.address?.country}
                            {...register("address.country", { required: true })}
                        />
                        {errors.address?.country && <span>To pole jest wymagane</span>}
                    </div>
                    <div>
                        <label htmlFor="postalCode">Postal Code:</label>
                        <input
                            id="postalCode"
                            defaultValue={user?.address?.postalCode}
                            {...register("address.postalCode", { required: true })}
                        />
                        {errors.address?.postalCode && <span>To pole jest wymagane</span>}
                    </div>
                    <button type="submit">Aktualizuj profil</button>
                </form>
            </div>
        </div>
    );
};

export default UserProfile;
