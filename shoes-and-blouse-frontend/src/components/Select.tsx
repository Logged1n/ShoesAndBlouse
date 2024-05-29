import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

export default function SelectLabel() {
    const [orderStatus, setOrderStatus] = React.useState('');

    const handleChange = (event: SelectChangeEvent) => {
        setOrderStatus(event.target.value);
    };

    return (
        <div>
            <FormControl sx={{m: 1, minWidth: 120}}>
                <InputLabel id="demo-simple-select-helper-label">Order Status</InputLabel>
                <Select
                    labelId="demo-simple-select-helper-label"
                    id="demo-simple-select-helper"
                    value={orderStatus}
                    label="Order Status"
                    onChange={handleChange}
                >
                    <MenuItem value="">
                        <em>Open</em>
                    </MenuItem>
                    <MenuItem value={0}>Open</MenuItem>
                    <MenuItem value={1}>Confirmed</MenuItem>
                    <MenuItem value={2}>Thirty</MenuItem>
                </Select>
            </FormControl>
        </div>
    );
}
