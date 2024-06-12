"use client"
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useForm, SubmitHandler } from 'react-hook-form';

interface UserForm {
    id: number;
    name?: string;
    email?: string;
    surname?: string;
    // Dodaj inne właściwości użytkownika, które chcesz wyświetlić
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
                    {/* Dodaj inne właściwości do wyświetlenia */}
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
                    <button type="submit">Aktualizuj profil</button>
                </form>
            </div>
        </div>
    );
};

export default UserProfile;
