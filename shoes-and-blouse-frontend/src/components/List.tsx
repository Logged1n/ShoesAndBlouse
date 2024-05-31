"use client"

import * as React from 'react';
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import IconButton from '@mui/material/IconButton';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import homepage from "../../public/homepage.jpg";
import Image from "next/image";

export default function InteractiveList() {
    const [dense, setDense] = React.useState(false);
    const [secondary, setSecondary] = React.useState(false);

    const items = [
        { id: 1, name: 'Item 1', image: homepage },
        { id: 2, name: 'Item 2', image: homepage }
    ];

    return (
        <Box sx={{ flexGrow: 1, maxWidth: 752 }}>
            <Grid container spacing={2}>
                <Grid item xs={12} md={6}>
                    <Typography sx={{ mt: 4, mb: 2 }} variant="h6" component="div">
                        Lista produktów
                    </Typography>
                    <List dense={dense}>
                        {items.map(item => (
                            <ListItem
                                key={item.id}
                                disableGutters
                            >
                                <Grid container spacing={2} alignItems="center">
                                    <Grid item>
                                        <Image
                                            src={item.image}
                                            alt="item pic"
                                            width={200}
                                            height={200}
                                        />
                                    </Grid>
                                    <Grid item xs>
                                        <ListItemText
                                            primary={item.name}
                                            secondary={secondary ? 'Secondary text' : null}
                                        />
                                    </Grid>
                                    <Grid item>
                                        <IconButton edge="end" aria-label="add-to-cart">
                                            <ShoppingCartIcon />
                                        </IconButton>
                                    </Grid>
                                </Grid>
                            </ListItem>
                        ))}
                    </List>
                </Grid>
            </Grid>
        </Box>
    );
}
