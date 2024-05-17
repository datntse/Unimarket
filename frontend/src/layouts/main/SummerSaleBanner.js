import React from 'react';
import { Box, Typography, Button, useTheme } from '@mui/material';
import { styled } from '@mui/system';

const StyledBanner = styled(Box)(({ theme }) => ({
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: theme.spacing(2),
    backgroundColor: theme.palette.error.main, // Customize color accordingly
    color: '#ffffff',
    borderRadius: theme.shape.borderRadius,
}));

const SaleInfo = styled(Box)({
    textAlign: 'left'
});

const ActionButton = styled(Button)(({ theme }) => ({
    backgroundColor: theme.palette.background.paper, // Light color for the button
    color: theme.palette.error.main, // Button text color
    '&:hover': {
        backgroundColor: theme.palette.background.default,
    },
}));

export default function SummerSaleBanner() {
    const theme = useTheme();

    return (
        <StyledBanner>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                {/* This could be an imported image or a custom SVG component */}
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    SUMMER SALE
                </Typography>
                <SaleInfo>
                    <Typography variant="h4" component="div">
                        30% OFF
                    </Typography>
                    <Typography variant="subtitle1">
                        Free on all your orders, Free Shipping and 30 days money-back guarantee
                    </Typography>
                </SaleInfo>
            </Box>
            <ActionButton variant="contained">
                Shop Now
            </ActionButton>
        </StyledBanner>
    );
}
