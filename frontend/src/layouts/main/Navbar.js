import React from 'react';
import { AppBar, Toolbar, Typography, Button, Grid, Card, CardActions, CardContent, Container, Stack } from '@mui/material';
import ArrowForwardOutlinedIcon from '@mui/icons-material/ArrowForwardOutlined';

function Navbar() {

    const imageUrl = '/logo/banner.png'
    const image = 'logo/image.png'
    const image1 = 'logo/image1.png'

    return (
        <div>
            <Container>

                <Grid container spacing={2} style={{ padding: 20, marginTop: 190 }}>
                    <Grid item xs={12} md={8}>
                        <Card sx={{
                            backgroundImage: `url(${imageUrl})`,
                            backgroundSize: 'cover',
                            backgroundPosition: 'center',
                            height: 400,
                            display: 'flex',
                            flexDirection: 'column',
                            justifyContent: 'center',
                            alignItems: 'start',
                            padding: 4
                        }}>
                            <CardContent
                            >
                                <Typography variant="h4" gutterBottom color="#c23908">
                                    Fashions
                                </Typography>
                                <Typography variant="subtitle1" color='#c43f10'>
                                    Sale up to <span style={{ backgroundColor: '#ffa436', color: 'white', padding: '2px 8px', borderRadius: 3 }}>30% OFF</span>
                                </Typography>
                                <Typography variant="subtitle1" color='#e09c83'>
                                    Free shipping on all order
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button variant='contained' size="small" sx={{ marginLeft: 2, backgroundColor: '#ffa436', color: 'white', }} endIcon={<ArrowForwardOutlinedIcon />}>
                                    Shop Now
                                </Button>
                            </CardActions>
                        </Card>
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <Stack spacing={2} justifyContent='space-around'>
                            <Card sx={{
                                backgroundImage: `url(${image})`,
                                backgroundSize: 'cover', // Đảm bảo hình ảnh phủ kín card
                                backgroundPosition: 'center', // Căn giữa hình ảnh
                                height: 192,
                                display: 'flex',
                                flexDirection: 'column',
                                justifyContent: 'space-evenly',
                                alignItems: 'start'
                            }}>
                                <Stack >
                                    <Stack sx={{
                                        ml: 2,
                                        marginBottom: 12,
                                    }}>
                                        <Typography variant="h6" color="textSecondary">
                                            Summer Sale
                                        </Typography>
                                        <Typography variant="h4">
                                            75% OFF
                                        </Typography>
                                    </Stack>
                                    <Button sx={{
                                        mr: 1,
                                        marginTop: -3,
                                    }} size="small" color="primary" endIcon={<ArrowForwardOutlinedIcon />}>
                                        Shop Now
                                    </Button>
                                </Stack>
                            </Card>
                            <Card sx={{
                                backgroundImage: `url(${imageUrl})`,
                                backgroundSize: 'cover', // Đảm bảo hình ảnh phủ kín card
                                backgroundPosition: 'center', // Căn giữa hình ảnh
                                height: 192,
                                display: 'flex',
                                flexDirection: 'column',
                                justifyContent: 'center',
                                alignItems: 'start'
                            }} >
                                <CardContent>
                                    <Typography variant="h5" color="textSecondary">
                                        Best idea
                                    </Typography>
                                    <Typography variant="h6">
                                        Special Products <Typography variant="h6">Deal of the Month</Typography>
                                    </Typography>
                                </CardContent>
                                <CardActions>
                                    <Button size="small" color="primary" endIcon={<ArrowForwardOutlinedIcon />}>
                                        Shop Now
                                    </Button>
                                </CardActions>
                            </Card>
                        </Stack>
                    </Grid>
                </Grid>
            </Container>
        </div >
    );
}

export default Navbar;
