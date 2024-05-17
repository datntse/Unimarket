import React from 'react';
import { styled } from '@mui/material/styles';
import InputBase from '@mui/material/InputBase';
import { IconButton, Box, Button } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';

const SearchBarContainer = styled(Box)(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    width: '100%',
    maxWidth: 629,
    border: '1px solid #ccc',
    borderRadius: theme.shape.borderRadius,
    boxShadow: '0 2px 6px rgba(0,0,0,0.1)',
    backgroundColor: '#ffffff' // Ensuring the background is white
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
    marginLeft: theme.spacing(1),
    flex: 1,
    fontSize: '1rem',
}));

const SearchButton = styled(Button)(({ theme }) => ({
    color: '#ffffff', // Setting text color to white
    backgroundColor: '#FF9800',
    padding: theme.spacing(1),
    '&:hover': {
        backgroundColor: '#F57C00', // Darken the color slightly on hover
    },
    borderTopRightRadius: theme.shape.borderRadius,
    borderBottomRightRadius: theme.shape.borderRadius,
    borderTopLeftRadius: 0,
    borderBottomLeftRadius: 0,
}));

export default function SearchBar() {
    return (
        <SearchBarContainer>
            <SearchIcon sx={{ ml: 1, color: 'action.active', mr: 1 }} />
            <StyledInputBase
                placeholder="Search"
                inputProps={{ 'aria-label': 'search google maps' }}
            />
            <SearchButton type="submit" aria-label="search">
                Search
            </SearchButton>
        </SearchBarContainer>
    );
}
